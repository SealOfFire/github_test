exec sp_rename 'tbl_Left.title','name'; -- 修改字段名称
alter table tbl_Left add insert_date datetimeoffset(7); -- 新增字段
alter table tbl_Left add insert_user uniqueidentifier; -- 新增字段
alter table tbl_Left add update_date datetimeoffset(7); -- 新增字段
alter table tbl_Left add update_user uniqueidentifier; -- 新增字段
alter table tbl_Left add delete_flag bit; -- 新增字段
alter table tbl_Left drop column last_update; -- 删除字段

exec sp_rename 'tbl_Right.title','name';  -- 修改字段名称
alter table tbl_Right add insert_date datetimeoffset(7); -- 新增字段
alter table tbl_Right add insert_user uniqueidentifier; -- 新增字段
alter table tbl_Right add update_date datetimeoffset(7); -- 新增字段
alter table tbl_Right add update_user uniqueidentifier; -- 新增字段
alter table tbl_Right add delete_flag bit; -- 新增字段
alter table tbl_Right drop column last_update; -- 删除字段
