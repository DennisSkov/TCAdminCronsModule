INSERT INTO tc_modules (module_id, display_name, version, enabled, config_page, component_directory,
                        security_class)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'Crons', '2.0', 1, null, null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 40, '07405876-e8c2-4b24-a774-4ef57f596384', 1,
        '/Crons/Configuration', '/Crons/Configuration', 'Crons', 'Configuration', 'Crons Configuration',
        'MenuIcons/Base/ServerComponents24x24.png', 'MenuIcons/Base/ServerComponents16x16.png', 1, 1000,
        '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', null, null,
        null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_panelbar_categories (category_id, module_id, display_name, view_order, parent_category_id,
                                    parent_module_id, page_id, panelbar_icon)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'Crons', 1001, 6, '07405876-e8c2-4b24-a774-4ef57f596384', null,
        null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_module_server_components (module_id, component_id, display_name, short_name, description,
                                         component_type, visible, component_class, required, startup_order)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 1, 'TCAdminCrons - Game Updates', 'tcacronsGU',
        'Game Updates automatic configuration', 1, 1, 'TCAdminCrons.TcAdminCronsService, TCAdminCrons', 1, 10);

INSERT INTO tc_server_enabled_components (module_id, component_id, server_id)
VALUES ('3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 1, 1);

# Configurations -------------------------------------------------------------------------------------------------------
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (1, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'VanillaSettings',
        'TCAdminCrons.Configuration.VanillaSettings, TCAdminCrons',
        '{"Enabled":false,"NameTemplate":"{Update.Version}","Group":"Vanilla Release","Description":"This is a vanilla server snapshot of Minecraft: Java Edition | Release Date: {Update.ReleaseTime}","UseVersionAsViewOrder":true,"GetLastReleaseUpdates":15,"FileName":"minecraft_server.jar","GameId":0,"ImageUrl":null,"ExtractPath":"/"}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
  <add key="__TCA:CREATED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:CREATED_ON" value="2020-09-25 16:58:52" type="System.DateTime,mscorlib" />
  <add key="__TCA:MODIFIED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:MODIFIED_ON" value="2020-09-28 17:04:33" type="System.DateTime,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (2, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'VanillaSnapshotSettings',
        'TCAdminCrons.Configuration.VanillaSnapshotSettings, TCAdminCrons',
        '{"Enabled":false,"NameTemplate":"{Update.Version}","Group":"Vanilla Snapshot Release","Description":"This is a vanilla snapshot server snapshot of Minecraft: Java Edition | Release Date: {Update.ReleaseTime} | Added by TCAdminCrons","UseVersionAsViewOrder":true,"GetLastReleaseUpdates":15,"FileName":"minecraft_server.jar","GameId":151,"ImageUrl":null,"ExtractPath":"/"}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
  <add key="__TCA:CREATED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:CREATED_ON" value="2020-09-25 16:58:52" type="System.DateTime,mscorlib" />
  <add key="__TCA:MODIFIED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:MODIFIED_ON" value="2020-09-29 13:53:06" type="System.DateTime,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (3, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'BukkitSettings',
        'TCAdminCrons.Configuration.BukkitSettings, TCAdminCrons',
        '{"Enabled":false,"Group":"Bukkit","NameTemplate":"{Update.Version}","Description":"CraftBukkit is lightly modified version of the Vanilla software allowing it to be able to run Bukkit plugins | Added by TCAdminCrons","UseVersionAsViewOrder":true,"GetLastReleaseUpdates":15,"FileName":"minecraft_server.jar","GameId":0,"ImageUrl":null,"ExtractPath":"/"}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
  <add key="__TCA:MODIFIED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:MODIFIED_ON" value="2020-10-14 11:36:11" type="System.DateTime,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (4, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'PaperSettings',
        'TCAdminCrons.Configuration.PaperSettings, TCAdminCrons',
        '{"Enabled":false,"Group":"Paper","NameTemplate":"{Update.Version}","Description":"Paper is the next generation of Minecraft server, compatible with Spigot plugins and offering uncompromising performance. | Added by TCAdminCrons","UseVersionAsViewOrder":true,"GetLastReleaseUpdates":15,"FileName":"minecraft_server.jar","GameId":0,"ImageUrl":null,"ExtractPath":"/"}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
  <add key="__TCA:MODIFIED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:MODIFIED_ON" value="2020-10-14 11:55:38" type="System.DateTime,mscorlib" />
</values>');
INSERT INTO ar_common_configurations (id, moduleId, name, typeName, contents, app_data)
VALUES (5, '3a0e1e17-cbee-4e00-871e-e3f492e8c8da', 'SpigotSettings',
        'TCAdminCrons.Configuration.SpigotSettings, TCAdminCrons',
        '{"Enabled":false,"Group":"Spigot","NameTemplate":"{Update.Version}","Description":"Spigot is a modified version of CraftBukkit with hundreds of improvements and optimizations that can only make CraftBukkit shrink in shame!","UseVersionAsViewOrder":true,"GetLastReleaseUpdates":15,"FileName":"minecraft_server.jar","GameId":0,"ImageUrl":null,"ExtractPath":"/"}', '<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<values>
  <add key="AR_COMMON:ConfigurationView" value="MinecraftGameUpdate" type="System.String,mscorlib" />
  <add key="__TCA:MODIFIED_BY" value="3" type="System.Int32,mscorlib" />
  <add key="__TCA:MODIFIED_ON" value="2020-09-28 17:04:18" type="System.DateTime,mscorlib" />
</values>');