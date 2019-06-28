/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     6/21/2019 11:18:08 AM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QA') and o.name = 'FK_QA_RELATIONS_QUESTION')
alter table QA
   drop constraint FK_QA_RELATIONS_QUESTION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QA') and o.name = 'FK_QA_RELATIONS_ANSWER')
alter table QA
   drop constraint FK_QA_RELATIONS_ANSWER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('QUESTION') and o.name = 'FK_QUESTION_RELATIONS_TEST')
alter table QUESTION
   drop constraint FK_QUESTION_RELATIONS_TEST
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERANSWERS') and o.name = 'FK_USERANSW_USERANSWE_QA')
alter table USERANSWERS
   drop constraint FK_USERANSW_USERANSWE_QA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERANSWERS') and o.name = 'FK_USERANSW_USERANSWE_USERTEST')
alter table USERANSWERS
   drop constraint FK_USERANSW_USERANSWE_USERTEST
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERTEST') and o.name = 'FK_USERTEST_RELATIONS_USER')
alter table USERTEST
   drop constraint FK_USERTEST_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERTEST') and o.name = 'FK_USERTEST_RELATIONS_TEST')
alter table USERTEST
   drop constraint FK_USERTEST_RELATIONS_TEST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ANSWER')
            and   type = 'U')
   drop table ANSWER
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('QA')
            and   name  = 'RELATIONSHIP_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index QA.RELATIONSHIP_6_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('QA')
            and   name  = 'RELATIONSHIP_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index QA.RELATIONSHIP_5_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('QA')
            and   type = 'U')
   drop table QA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('QUESTION')
            and   name  = 'RELATIONSHIP_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index QUESTION.RELATIONSHIP_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('QUESTION')
            and   type = 'U')
   drop table QUESTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TEST')
            and   type = 'U')
   drop table TEST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"USER"')
            and   type = 'U')
   drop table "USER"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USERANSWERS')
            and   name  = 'USERANSWERS_FK'
            and   indid > 0
            and   indid < 255)
   drop index USERANSWERS.USERANSWERS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USERANSWERS')
            and   type = 'U')
   drop table USERANSWERS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USERTEST')
            and   name  = 'RELATIONSHIP_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index USERTEST.RELATIONSHIP_3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USERTEST')
            and   name  = 'RELATIONSHIP_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index USERTEST.RELATIONSHIP_2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USERTEST')
            and   type = 'U')
   drop table USERTEST
go

/*==============================================================*/
/* Table: ANSWER                                                */
/*==============================================================*/
create table ANSWER (
   ANSWERID             int                  not null,
   ANSWERTEXT           varchar(200)         null,
   ANSWERNOTE           varchar(150)         null,
   constraint PK_ANSWER primary key nonclustered (ANSWERID)
)
go

/*==============================================================*/
/* Table: QA                                                    */
/*==============================================================*/
create table QA (
   QA_ID                int                  not null,
   QUESTIONID           int                  null,
   ANSWERID             int                  null,
   CORRECTANSWER        bit                  null,
   constraint PK_QA primary key nonclustered (QA_ID)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_5_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_5_FK on QA (
QUESTIONID ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_6_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_6_FK on QA (
ANSWERID ASC
)
go

/*==============================================================*/
/* Table: QUESTION                                              */
/*==============================================================*/
create table QUESTION (
   QUESTIONID           int                  not null,
   TESTID               int                  not null,
   QUESTIONTEXT         varchar(200)         null,
   QUESTIONNOTE         varchar(150)         null,
   constraint PK_QUESTION primary key nonclustered (QUESTIONID)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_1_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_1_FK on QUESTION (
TESTID ASC
)
go

/*==============================================================*/
/* Table: TEST                                                  */
/*==============================================================*/
create table TEST (
   TESTID               int                  not null,
   TESTCATEGORY         varchar(50)          null,
   TESTTITLE            varchar(50)          null,
   TESTNOTE             varchar(200)         null,
   constraint PK_TEST primary key nonclustered (TESTID)
)
go

/*==============================================================*/
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   USERID               int                  not null,
   LNAME                varchar(50)          null,
   FNAME                varchar(50)          null,
   EMAIL                varchar(50)          null,
   constraint PK_USER primary key nonclustered (USERID)
)
go

/*==============================================================*/
/* Table: USERANSWERS                                           */
/*==============================================================*/
create table USERANSWERS (
   USERTESTID           int                  not null,
   QA_ID                int                  not null,
   constraint PK_USERANSWERS primary key nonclustered (USERTESTID, QA_ID)
)
go

/*==============================================================*/
/* Index: USERANSWERS_FK                                        */
/*==============================================================*/
create unique index USERANSWERS_FK on USERANSWERS (
QA_ID ASC
)
go

/*==============================================================*/
/* Table: USERTEST                                              */
/*==============================================================*/
create table USERTEST (
   USERTESTID           int                  not null,
   USERID               int                  not null,
   TESTID               int                  not null,
   TESTDATE             datetime             null,
   TESTREMARKS          varchar(150)         null,
   constraint PK_USERTEST primary key nonclustered (USERTESTID)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_2_FK on USERTEST (
USERID ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_3_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_3_FK on USERTEST (
TESTID ASC
)
go

alter table QA
   add constraint FK_QA_RELATIONS_QUESTION foreign key (QUESTIONID)
      references QUESTION (QUESTIONID)
go

alter table QA
   add constraint FK_QA_RELATIONS_ANSWER foreign key (ANSWERID)
      references ANSWER (ANSWERID)
go

alter table QUESTION
   add constraint FK_QUESTION_RELATIONS_TEST foreign key (TESTID)
      references TEST (TESTID)
go

alter table USERANSWERS
   add constraint FK_USERANSW_USERANSWE_QA foreign key (QA_ID)
      references QA (QA_ID)
go

alter table USERANSWERS
   add constraint FK_USERANSW_USERANSWE_USERTEST foreign key (USERTESTID)
      references USERTEST (USERTESTID)
go

alter table USERTEST
   add constraint FK_USERTEST_RELATIONS_USER foreign key (USERID)
      references "USER" (USERID)
go

alter table USERTEST
   add constraint FK_USERTEST_RELATIONS_TEST foreign key (TESTID)
      references TEST (TESTID)
go

