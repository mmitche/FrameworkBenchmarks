FROM node:10.12.0

COPY ./ ./

ADD https://github.com/aspnet/Benchmarks/raw/master/src/Benchmarks/testCert.pfx ./

RUN npm install

ENV NODE_HANDLER mysql-raw

RUN openssl version

CMD ["node", "app.js"]
