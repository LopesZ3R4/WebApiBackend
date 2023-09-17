#!/bin/bash

set -e

host="$1"
port="$2"
shift
shift
cmd="$@"

until nc -z $host $port; do
  >&2 echo "SQL Server is unavailable - sleeping"
  sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $cmd