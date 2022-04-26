docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' db
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' sv1
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' sv2
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' lb
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' cl