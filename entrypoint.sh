# ./entrypoint.sh

#!/bin/bash

# Start SQL Server
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
sleep 30s

# Check if this is the first run
if [ ! -f /app/first-run-complete ]; then
  # Run the initialization script
  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "@Senhamuitoforte1234" -d master -i init-script.sql

  # Create a flag file to indicate first run is complete
  touch /app/first-run-complete
fi

# Keep the container running
tail -f /dev/null