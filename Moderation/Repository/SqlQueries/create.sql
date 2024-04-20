Drop table AnsweredQuestion
drop table GroupQuestion
drop table JoinRequest
drop table Post
drop table Report
drop table MutedUser
drop table BannedUser
drop table Report
drop table GroupUser
drop table RolePermission
drop table Permission
drop table UserRole

CREATE TABLE UserRole(
	roleId int PRIMARY KEY,
	name varchar(250)
)
CREATE TABLE Permission(
	permissionId int PRIMARY KEY,
	name varchar(250)
)
CREATE TABLE RolePermission(
	roleId int references UserRole(roleId),
	permissionId int references Permission(permissionId),
	Primary Key (roleId,permissionId)
)
CREATE TABLE GroupUser(
	userId varchar(250) PRIMARY KEY,
	roleId int references UserRole(roleId),
	userPostScore int,
	userMarketplaceScore int,
	status varchar(250),
	name varchar(250)
)
CREATE TABLE Post(
	postId varchar(250) primary key,
	userId varchar(250) references GroupUser(userId),
	postScore int,
	status varchar(250)
)
CREATE TABLE Report(
	reportId varchar(250) primary key,
	userId varchar(250) references GroupUser(userId),
	message varchar(250),
	status varchar(250),
	type varchar(250)
)
CREATE TABLE BannedUser(
	bannedId varchar(250) primary key,
	userId varchar(250) references GroupUser(userId),
	message varchar(250),
	startTime DATETIME,
	endTime DATETIME
)
CREATE TABLE MutedUser(
	mutedId varchar(250) primary key,
	userId varchar(250) references GroupUser(userId),
	message varchar(250),
	startTime DATETIME,
	endTime DATETIME
)
CREATE TABLE JoinRequest(
	joinId varchar(250) primary key,
	userId varchar(250) references GroupUser(UserId),
	status varchar(250)
)
CREATE TABLE GroupQuestion(
	questionId int primary key,
	question varchar(250),
	typeofAnswer varchar(250)
)
CREATE TABLE AnsweredQuestion(
	joinId varchar(250) references JoinRequest(joinId),
	questionId int references GroupQuestion(questionId),
	asnwer varchar(250),
	primary key(joinId,questionId)
)
