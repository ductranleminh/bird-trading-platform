# bird-trading-system-backend
## Run project bằng docker (linux)
Cài docker trên máy trước

Mở cmd/terminal và cd đến project
```
docker build -t backend -f bird-trading/Dockerfile .
```
run tiếp
```
 docker run -d -p 31081:31081 backend  
```
