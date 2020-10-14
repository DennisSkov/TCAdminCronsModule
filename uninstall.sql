DELETE FROM tc_modules WHERE module_id LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM tc_site_map WHERE module_id LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM tc_panelbar_categories WHERE module_id LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM tc_module_server_components WHERE module_id LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM tc_server_enabled_components WHERE module_id LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM ar_common_configurations WHERE moduleId LIKE '3a0e1e17-cbee-4e00-871e-e3f492e8c8da';
DELETE FROM tc_info WHERE name LIKE 'Crons.MinecraftUpdates';
DROP TABLE tcmodule_cron_jobs;