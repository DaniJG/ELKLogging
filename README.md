# ELKLogging

This repo contains a sample application integrating Elasticsearch and .Net Core.

- A REST API for products is implemented which uses Elasticsearch as the backing service for storing and querying the data.
- A new logger provider is also implemented which sends the logged events to Elasticsearch.

If you use docker, you can easily start a new container with Elasticsearch and Kibana for testing purposes using:

     docker run -it --rm -p 9200:9200 -p 5601:5601 --name esk nshou/elasticsearch-kibana
