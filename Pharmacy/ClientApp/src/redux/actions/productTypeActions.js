import * as types from "./actionTypes";
import * as productTypeApi from "../../api/productTypeApi";

export function createProductType(productType) {
  return { type: types.CREATE_PRODUCT_TYPE, productType };
}

function loadProductTypesSuccess(productTypes) {
  return { type: types.LOAD_PRODUCT_TYPES_SUCCESS, productTypes };
}

export function loadProductTypes() {
  return function (dispatch) {
    return productTypeApi
      .getProductTypes()
      .then((productTypes) => {
        dispatch(loadProductTypesSuccess(productTypes));
      })
      .catch((error) => {
        throw error;
      });
  };
}
