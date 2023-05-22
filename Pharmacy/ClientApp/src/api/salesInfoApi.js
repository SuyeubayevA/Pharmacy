import { handleResponse, handleError } from "./apiUtils";
import * as urls from "../apiUrl";

const baseUrl = urls.API_URL + "salesinfo";

export function getSalesInfos() {
  return fetch(baseUrl).then(handleResponse).catch(handleError);
}
