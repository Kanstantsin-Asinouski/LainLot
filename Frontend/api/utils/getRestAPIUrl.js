export const getRestAPIUrl = (service) => {
  const VITE_RESTAPI_URL_DEV = process.env.VITE_RESTAPI_URL_DEV;
  const VITE_RESTAPI_URL_PROD = process.env.VITE_RESTAPI_URL_PROD;
  const VITE_AUTH_URL_DEV = process.env.VITE_AUTH_URL_DEV;
  const VITE_AUTH_URL_PROD = process.env.VITE_AUTH_URL_PROD;
  const VITE_ENVIRONMENT = process.env.VITE_ENVIRONMENT;


  switch (service) {
    case "rest":

      return VITE_ENVIRONMENT === 'Development'
        ? VITE_RESTAPI_URL_DEV
        : VITE_RESTAPI_URL_PROD;

    case "auth":

      return VITE_ENVIRONMENT === 'Development'
        ? VITE_AUTH_URL_DEV
        : VITE_AUTH_URL_PROD;

    default:
      break;
  }
};
