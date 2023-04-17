import { handleResponse, handleError } from "./apiUtils";
import * as urls from "../apiUrl";

const baseUrl = urls.API_URL + "productamount";

export function getDiscounts() {
  return fetch(baseUrl).then(handleResponse).catch(handleError);
}
