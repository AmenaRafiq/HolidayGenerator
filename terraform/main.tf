terraform {
    required_providers {
        azurerm = {
            source  = "hashicorp/azurerm"
            version = "~> 2.46.0"
        }
    }
}

provider "azurerm" {
    features {}
}

resource "azurerm_resource_group" "main" {
  name     = "${var.project_name}-rg"
  location = var.location
  tags = {
      project = "true"
  }
}

resource "azurerm_mysql_server" "main" {
  name                = "${var.project_name}-sqlserver"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name

  administrator_login          = "amenarafiq"
  administrator_login_password = "Holiday123"

  sku_name   = "B_Gen5_2"
  storage_mb = 5120
  version    = "5.7"

  auto_grow_enabled                 = true
  backup_retention_days             = 7
  geo_redundant_backup_enabled      = false
  infrastructure_encryption_enabled = false
  public_network_access_enabled     = true
  ssl_enforcement_enabled           = true
  ssl_minimal_tls_version_enforced  = "TLS1_2"

  tags = {
      project = "true"
  }
}

resource "azurerm_mysql_database" "main" {
  name                = "amenadb"
  resource_group_name = azurerm_resource_group.main.name
  server_name         = azurerm_mysql_server.main.name
  charset             = "utf8"
  collation           = "utf8_unicode_ci"

}

resource "azurerm_app_service_plan" "main" {
  name                = "amena-appserviceplan"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  kind = "Linux"
  reserved = true

  sku {
    tier = "Basic"
    size = "B1"
  }

  tags = {
      project = "true"
  }
}

resource "azurerm_app_service" "days" {
  name                = "${var.project_name}-days"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.main.id

  site_config {
    linux_fx_version       = "DOTNETCORE|5.0"
    dotnet_framework_version = "v5.0"
    scm_type                 = "None"
  }

  tags = {
      project = "true"
  }
}

resource "azurerm_app_service" "month" {
  name                = "${var.project_name}-month"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.main.id

  site_config {
    linux_fx_version       = "DOTNETCORE|5.0"
    dotnet_framework_version = "v5.0"
    scm_type                 = "None"
  }

  tags = {
      project = "true"
  }
}

resource "azurerm_app_service" "merge" {
  name                = "${var.project_name}-merge"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.main.id

  site_config {
    linux_fx_version       = "DOTNETCORE|5.0"
    dotnet_framework_version = "v5.0"
    scm_type                 = "None"
  }

  tags = {
      project = "true"
  }

  app_settings = {
    "daysServiceURL" = "https://amena-holidayapp-days.azurewebsites.net"
    "monthServiceURL" = "https://amena-holidayapp-month.azurewebsites.net"
  }

}

resource "azurerm_app_service" "frontend" {
  name                = "${var.project_name}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.main.id

  site_config {
    linux_fx_version       = "DOTNETCORE|5.0"
    dotnet_framework_version = "v5.0"
    scm_type                 = "None"
  }

  tags = {
      project = "true"
  }

  app_settings = {
    "mergeServiceURL" = "https://amena-holidayapp-merge.azurewebsites.net"
  }

  connection_string {
    name  = "DefaultConnection"
    type  = "SQLServer"
    value = "Database=${azurerm_mysql_database.main.name}; Data Source=amena-holidayapp-sqlserver.mysql.database.azure.com; User Id=amenarafiq@amena-holidayapp-sqlserver; Password=Holiday123"
  }
}

