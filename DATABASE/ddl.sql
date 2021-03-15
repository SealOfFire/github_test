CREATE DATABASE github_test;

CREATE TABLE tbl_Left(
	id int NOT NULL,
	title nvarchar(50) NULL,
	url nvarchar(2000) NULL,
	html_url nvarchar(2000) NULL,
	last_update datetimeoffset(7) NULL
    PRIMARY KEY (id)
);

CREATE TABLE tbl_Right(
	id int NOT NULL,
	title nvarchar(50) NULL,
	url nvarchar(2000) NULL,
	html_url nvarchar(2000) NULL,
	last_update datetimeoffset(7) NULL
    PRIMARY KEY (id)
);
