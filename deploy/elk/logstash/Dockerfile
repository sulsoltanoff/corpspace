# https://github.com/elastic/logstash-docker
FROM docker.elastic.co/logstash/logstash-oss:6.0.0

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
# RUN logstash-plugin install logstash-input-http

# Install the http input plugin from a local file
ADD logstash-input-http-3.6.1.tar.gz /usr/share/logstash/plugins/

COPY config/logstash.yml /usr/share/logstash/config/
COPY pipeline/ /usr/share/logstash/pipeline/