# Dockerfile.sql
FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

RUN mkdir -p /app
WORKDIR /app

COPY init-script.sql .
COPY entrypoint.sh .

RUN chmod +x entrypoint.sh

CMD /app/entrypoint.sh