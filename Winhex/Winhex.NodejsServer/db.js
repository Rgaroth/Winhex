var Sequelize = require("sequelize");
const colors = require('colors');
const md5 = require('md5');

var context = new Sequelize("UserLogs", "lzlzlfybk", "vbjy73ert", {
	dialect: "mssql",
	host: "UserLogs.mssql.somee.com",
	define: {
		timestamps: false,
		freezeTableName: true
	}
});

// модель Settings
const Settings = context.define("Settings", {
	Id: {
		type: Sequelize.INTEGER,
		autoIncrement: true,
		primaryKey: true,
		allowNull: false
	},
	ParameterName: {
		type: Sequelize.STRING
	},
	ParameterValue: {
		type: Sequelize.STRING
	}
});

// модель UserLog
const UserLog = context.define("UserLog", {
	Id: {
		type: Sequelize.INTEGER,
		autoIncrement: true,
		primaryKey: true,
		allowNull: false
	},
	CompName: {
		type: Sequelize.STRING
	},
	CustomNote: {
		type: Sequelize.STRING
	}
});

// модель UserAction
const UserAction = context.define("UserAction", {
	Id: {
		type: Sequelize.INTEGER,
		autoIncrement: true,
		primaryKey: true,
		allowNull: false
	},
	ActionDateTime: {
		type: Sequelize.STRING
	},
	AppTitle: {
		type: Sequelize.STRING
	},
	TextLog: {
		type: Sequelize.STRING
	},
	LogId: {
		type: Sequelize.INTEGER
	}
});

UserLog.hasMany(UserAction, { foreignKey: 'LogId', sourceKey: 'Id' });

module.exports.AddUserLog = function(log){
	var realLog = log;
	var realAction = log.Logs;
	
	UserLog.findOne({where:{CompName: log.CompName}}).then(existUserLog=>{
		console.log(existUserLog);
		
		if (existUserLog == null){
			UserLog.create(realLog).then(res=>{
				console.log(res);
				
				console.log("ID " + res.Id);
				realAction[0].LogId = res.Id;
				
				UserAction.create(realAction[0]).then(r=>{
					console.log("Action is added".green);
				});
			});
		}
		else{
			realLog = existUserLog;
			
			realAction.forEach(function(item, realAction){
				AddAction(item, realLog.Id);
			});
		}
		
		
	}).catch(err=>console.log(err));
}

function AddAction(action, id){
	action.LogId = id;
				
	UserAction.create(action).then(r=>{
		console.log("Action is added".green);
	});
}

module.exports.GetUsers = function(req, res){
	UserLog.findAll()
	.then(users=>{
		console.log(users);
		res.send(users);
		res.end();
	}).catch(err=> {
		console.log(err); 
		res.end();
	});
}

module.exports.GetUser = function(req, resp){
	var i = 0;
	Settings.findOne({where: {ParameterName: "UsersKey"}}).then(key=>{
		if (md5(req.params["key"]) != key.ParameterValue){
			resp.end();
		}
		else{
			var userLog;
			UserLog.findOne({where: {Id: req.params["id"]}}).then(res=>{
				userLog = { Id: res.Id, CompName: res.CompName, CustomNote: res.CustomNote };
				userLog.Logs = [];
				res.getUserActions().then(results=>{
					results.forEach(function(result, results){
						userLog.Logs[i++] = { Id: result.Id, ActionDateTime: result.ActionDateTime, 
							AppTitle: result.AppTitle, TextLog: result.TextLog, LogId: result.LogId
						};
					});
					
					resp.send(userLog);
					resp.end();
				});
				
			});
		}
	}).catch(err=>console.log(err));
}
