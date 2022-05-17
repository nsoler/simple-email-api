FROM mcr.microsoft.com/mssql/server:2019-CU15-ubuntu-20.04

ENV SA_PASSWORD SecretPass123
ENV ACCEPT_EULA Y
ENV MSSQL_PID Developer

COPY . .

EXPOSE 1433
CMD /bin/bash ./entrypoint.sh