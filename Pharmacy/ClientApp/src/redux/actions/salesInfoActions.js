import * as types from "./actionTypes";
import * as salesInfoApi from "../../api/salesInfoApi";

export function createSalesInfo(salesInfo) {
  return { type: types.CREATE_SALES_INFO, salesInfo };
}

export function loadSalesInfosSuccess(salesInfos) {
  return { type: types.LOAD_SALES_INFOS_SUCCESS, salesInfos };
}

export function loadSalesInfos() {
  return function (dispatch) {
    return salesInfoApi
      .getSalesInfos()
      .then((salesInfos) => {
        dispatch(loadSalesInfosSuccess(salesInfos));
      })
      .catch((error) => {
        throw error;
      });
  };
}
