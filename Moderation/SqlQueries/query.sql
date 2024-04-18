drop table [Rule]
drop table Vote
drop table JoinRequestMessage
drop table JoinRequest
drop table PollAward
drop table PostAward
drop table Report
drop table Award
drop table PollOption
drop table PollPost
drop table Post
drop table GroupUser
drop table [Group]
drop table [User]
drop table RolePermission
drop table UserRole

CREATE TABLE UserRole(
	RoleId UNIQUEIDENTIFIER PRIMARY KEY,
	Name nvarchar(255)
)
CREATE TABLE RolePermission(
	RoleId UNIQUEIDENTIFIER references UserRole(RoleId),
	Permission nvarchar(255),
	Primary Key (RoleId,Permission)
)
CREATE TABLE [User](
	Id UNIQUEIDENTIFIER Primary key,
	Username nvarchar(255),
	Password nvarchar(255)
)
CREATE TABLE [Group](
	Id UNIQUEIDENTIFIER primary key,
	Name NVARCHAR(255),
	Description NVARCHAR(MAX),
	Owner UNIQUEIDENTIFIER references [User](Id)
)

CREATE TABLE GroupUser(
    Id UNIQUEIDENTIFIER primary key,
    Uid UNIQUEIDENTIFIER references [User](Id),
	GroupId UNIQUEIDENTIFIER references [Group](Id),
    PostScore INT,
    MarketplaceScore INT,
    StatusRestriction INT, 
	StatusRestrictionDate DATETIME,
    StatusMessage NVARCHAR(MAX),
	RoleId UNIQUEIDENTIFIER references UserRole(RoleId)
)
CREATE TABLE Post(
	PostId UNIQUEIDENTIFIER primary key,
	Content nvarchar(MAX),
	UserId UNIQUEIDENTIFIER references GroupUser(Id),
	Score int,
	Status nvarchar(250),
	IsDeleted BIT,
	GroupId UNIQUEIDENTIFIER references [Group](Id)
)
CREATE TABLE PollPost (
    PollId UNIQUEIDENTIFIER PRIMARY KEY,
    Content NVARCHAR(MAX),
    UserId UNIQUEIDENTIFIER references GroupUser(Id),
    Score INT,
    Status NVARCHAR(50),
    IsDeleted BIT,
	GroupId UNIQUEIDENTIFIER references [Group](Id)
)
CREATE TABLE PollOption (
    OptionId INT IDENTITY PRIMARY KEY,
    PollId UNIQUEIDENTIFIER references PollPost(PollId),
    OptionText NVARCHAR(MAX),
)
CREATE TABLE Award(
	AwardId UNIQUEIDENTIFIER primary key,
	Type nvarchar(255)
)
CREATE TABLE PostAward(
	AwardId UNIQUEIDENTIFIER references Award(AwardId),
	PostId UNIQUEIDENTIFIER references Post(PostId),
	primary key(AwardId,PostId)
)
CREATE TABLE PollAward(
	AwardId UNIQUEIDENTIFIER references Award(AwardId),
	PollId UNIQUEIDENTIFIER references PollPost(PollId),
	primary key (AwardId,PollId)
)
CREATE TABLE Report(
	ReportId UNIQUEIDENTIFIER primary key,
	UserId UNIQUEIDENTIFIER references GroupUser(Id),
	PostId UNIQUEIDENTIFIER references Post(PostId),
	Message varchar(250),
	GroupId UNIQUEIDENTIFIER references [Group](Id)
)
CREATE TABLE JoinRequest(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER references GroupUser(Id)
)
CREATE TABLE JoinRequestMessage (
    JoinRequestId UNIQUEIDENTIFIER references JoinRequest(Id),
    [Key] NVARCHAR(255),
    [Value] NVARCHAR(MAX),
	primary key(JoinRequestId,[Key])
)
CREATE TABLE Vote(
	VoteId UNIQUEIDENTIFIER primary key,
	UserPost UNIQUEIDENTIFIER references GroupUser(Id),
	PollId UNIQUEIDENTIFIER references PollPost(PollId),
	Options nvarchar(Max)
)
CREATE TABLE [Rule](
	RuleId UNIQUEIDENTIFIER primary key,
	GroupId UNIQUEIDENTIFIER references [Group](Id),
	Title nvarchar(255),
	Text nvarchar(MAX)
)