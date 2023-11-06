import config from "../environements";

const BASE_URL = config.BASE_URL;
const DEFAULT_HEADERS = {
  "Content-Type": "application/json",
};

export async function fetchData(endpoint, customHeaders = {}, body = {}) {
  const URL = `${BASE_URL}/${endpoint}`;

  const HEADERS = {
    ...DEFAULT_HEADERS,
    ...customHeaders,
  };

  try {
  } catch (err) {
    console.log(err);
  }
}
