# Instalación del servidor de XMPP (Prosody)

En este caso, decidí instalar un Prosody en el WSL (Ubuntu) de mi Windows 10. Abro una consola.

    sudo apt-get install prosody

Luego editamos el archivo de configuración

    sudo vim /etc/prosody/prosody.cfg.lua

    -- These modules are auto-loaded, but should you want
    -- to disable them then uncomment them here:
    modules_disabled = {
            -- "offline"; -- Store offline messages
            --"c2s"; -- Handle client connections
            "s2s"; -- Handle server-to-server connections
    }

    -- Force clients to use encrypted connections? This option will
    -- prevent clients from authenticating unless they are using encryption.

    c2s_require_encryption = false

    ----------- Virtual hosts -----------
    -- You need to add a VirtualHost entry for each domain you wish Prosody to serve.
    -- Settings under each VirtualHost entry apply *only* to that host.

    VirtualHost "localhost"
            certificate = "/var/lib/prosody/localhost.crt"


sudo prosodyctl cert generate localhost

sudo /etc/init.d/prosody start

sudo prosodyctl register auction-item-65432 localhost auction
sudo prosodyctl register auction-item-54321 localhost auction
sudo prosodyctl register sniper localhost sniper

