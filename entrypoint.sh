#file:///c%3A/WebApiBackend/entrypoint.sh

#!/bin/bash

# Start SQL Server
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
sleep 30s

# Run the initialization script
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "@Senhamuitoforte1234" -d master -i init-script.sql

# Keep the container running
tail -f /dev/null