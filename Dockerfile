FROM debian:9 

RUN apt-get update -yq \
   && apt-get install curl gnupg -yq \
   && curl -sL https://deb.nodesource.com/setup_10.x | bash \
   && apt-get install nodejs -yq \
   && apt-get clean -y

ADD . /app/
WORKDIR /app
RUN npm install
WORKDIR  /app/site/
RUN npm install -g @angular/cli
RUN npm install
RUN ng build --configuration=local
WORKDIR /app
# EXPOSE 2368
VOLUME /app/logs

CMD npm run docker