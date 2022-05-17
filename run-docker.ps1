Start-Process "http://localhost/api/v1/email/healthcheck"

docker run -it --rm -p 80:80 --name emailapi-run emailapi:0.1
