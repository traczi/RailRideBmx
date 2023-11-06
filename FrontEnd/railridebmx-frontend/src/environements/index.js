import developmentConfig from "./development";
import productionConfig from "./production";

const env = process.env.NODE_ENV || "development";

const EnvEnum = {
  DEVELOPMENT: "development",
  PRODUCTION: "production",
};

let config;

switch (env) {
  case EnvEnum.DEVELOPMENT:
    config = developmentConfig;
    break;
  case EnvEnum.PRODUCTION:
    config = productionConfig;
    break;
  default:
    throw Error("TODO");
}

export default config;
