INSERT INTO tc_modules (module_id, display_name, version, enabled, config_page, component_directory,
                        security_class)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'Crons', '2.0', 1, null, null, null);

-- ---------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 40, '07405876-e8c2-4b24-a774-4ef57f596384', 1,
        '/Crons/Configuration', '/Crons/Configuration', 'Crons', 'Configuration', 'Crons Configuration',
        'MenuIcons/Base/ServerComponents24x24.png', 'MenuIcons/Base/ServerComponents16x16.png', 1, 1000,
        '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', null, null,
        null);

-- ---------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_panelbar_categories (category_id, module_id, display_name, view_order, parent_category_id,
                                    parent_module_id, page_id, panelbar_icon)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'Crons', 1001, 6, '07405876-e8c2-4b24-a774-4ef57f596384', null,
        null);

-- ---------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_module_server_components (module_id, component_id, display_name, short_name, description,
                                         component_type, visible, component_class, required, startup_order)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 1, 'TCAdminCrons - Game Updates', 'tcacronsGU',
        'Game Updates automatic configuration', 1, 1, 'TCAdminCrons.TcAdminCronsService, TCAdminCrons', 1, 10);

INSERT INTO tc_server_enabled_components (module_id, component_id, server_id)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 1, 1);

-- Tables --------------------------------------------------------------------------------------------------------------

create table tcmodule_cron_jobs
(
    id                    int auto_increment
        primary key,
    typeName              text        null,
    configurationModuleId varchar(36) not null,
    configurationId       int         null,
    executeOrder          int         null,
    executeEverySeconds   int         null,
    app_data              text        null
);

INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (1, 'TCAdminCrons.Crons.GameUpdates.MinecraftVanillaUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 1, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');
INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (2, 'TCAdminCrons.Crons.GameUpdates.MinecraftVanillaSnapshotUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 2, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');
INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (3, 'TCAdminCrons.Crons.GameUpdates.MinecraftBukkitUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 3, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');
INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (4, 'TCAdminCrons.Crons.GameUpdates.MinecraftPaperUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 4, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');
INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (5, 'TCAdminCrons.Crons.GameUpdates.MinecraftSpigotUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 5, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');
INSERT INTO tcmodule_cron_jobs (id, typeName, configurationModuleId, configurationId, executeOrder,
                                        executeEverySeconds, app_data)
VALUES (4, 'TCAdminCrons.Crons.GameUpdates.MinecraftPurpurUpdatesCron, TCAdminCrons',
        '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 6, 1, 3600, '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="Group" value="Minecraft" type="System.String,mscorlib" />
</values>');

-- Configurations ------------------------------------------------------------------------------------------------------

INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'VanillaSettings',
        'TCAdminCrons.Configuration.VanillaSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (2, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'VanillaSnapshotSettings',
        'TCAdminCrons.Configuration.VanillaSnapshotSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (3, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'BukkitSettings',
        'TCAdminCrons.Configuration.BukkitSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (4, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'PaperSettings',
        'TCAdminCrons.Configuration.PaperSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (5, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'SpigotSettings',
        'TCAdminCrons.Configuration.SpigotSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (6, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'PurpurSettings',
        'TCAdminCrons.Configuration.PurpurSettings, TCAdminCrons',
        '{}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
</values>');
