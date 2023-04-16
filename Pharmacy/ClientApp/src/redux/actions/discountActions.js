import * as types from "./actionTypes";
import * as discountApi from "../../api/discountApi";

export function createDiscount(discount) {
  return { type: types.CREATE_PRODUCTS_DISCOUNT, discount };
}

export function loadDiscountsSuccess(discounts) {
  return { type: types.LOAD_PRODUCTS_DISCOUNT_SUCCESS, discounts };
}

export function loadDiscounts() {
  return function (dispatch) {
    return discountApi
      .getDiscounts()
      .then((discounts) => {
        dispatch(loadDiscountsSuccess(discounts));
      })
      .catch((error) => {
        throw error;
      });
  };
}
