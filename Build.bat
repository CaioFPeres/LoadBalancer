docker build -f Dockerfile.db -t db .
docker build -f Dockerfile.sv1 -t sv1 .
docker build -f Dockerfile.sv2 -t sv2 .
docker build -f Dockerfile.lb -t lb .
docker build -f Dockerfile.cl -t cl .