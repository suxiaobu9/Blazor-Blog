# Docker Containers 之間相互溝通

在 Docker 中，有自己的虛擬網路 Software-Defined Networks (SDN)，當 Image 執行成 Container 時，就會預設加入這個虛擬網路，而這個虛擬網路就能夠讓 Container 相互溝通。

```cmd
> docker network ls
```

[![image](https://user-images.githubusercontent.com/37999690/128209882-77afcccf-a28a-4013-bb7c-9155da406baa.png "image")](https://user-images.githubusercontent.com/37999690/128209882-77afcccf-a28a-4013-bb7c-9155da406baa.png)

## 下以下指令，裝一個 M$ Sql Server

```cmd
> docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 --name DevSql -d mcr.microsoft.com/mssql/server
```

[![image](https://user-images.githubusercontent.com/37999690/128211985-bf69ec7e-9faa-46c4-a7db-1edd2f839fdb.png "image")](https://user-images.githubusercontent.com/37999690/128211985-bf69ec7e-9faa-46c4-a7db-1edd2f839fdb.png)

## 下指令取得 DevSql 的虛擬 IP

```cmd
> docker network inspect bridge
```

[![image](https://user-images.githubusercontent.com/37999690/128212445-cc9f7945-da00-4176-a8d8-694ca9decb9a.png "image")](https://user-images.githubusercontent.com/37999690/128212445-cc9f7945-da00-4176-a8d8-694ca9decb9a.png)

之後再其他的 Container，只要敲 `172.17.0.2` 就可以與 DevSql 溝通了
