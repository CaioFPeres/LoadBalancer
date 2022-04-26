docker run -p 27017:27017 -it -d --name db db
docker run -p 5000:5000 -it -d --name sv1 sv1
docker run -p 5001:5001 -it -d --name sv2 sv2
docker run -p 3000:3000 -it -d --name lb lb
docker run -d --name cl cl