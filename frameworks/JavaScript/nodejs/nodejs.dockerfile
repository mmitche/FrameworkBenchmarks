FROM node:10.12.0

COPY ./ ./

COPY https://github.com/aspnet/Benchmarks/raw/master/src/Benchmarks/testCert.pfx ./testCert.pfx

RUN npm install

ENV NODE_HANDLER mysql-raw

CMD ["node", "app.js"]
