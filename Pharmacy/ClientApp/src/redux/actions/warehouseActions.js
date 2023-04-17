import * as types from "./actionTypes";
import * as warehouseApi from "../../api/warehouseApi";

export function createWarehouse(warehouse) {
  return { type: types.CREATE_WAREHOUSE, warehouse };
}

function loadWarehouseSuccess(warehouses) {
  return { type: types.LOAD_WAREHOUSES_SUCCESS, warehouses };
}

export function loadWarehouses() {
  return function (dispatch) {
    return warehouseApi
      .getWarehouses()
      .then((warehouses) => {
        dispatch(loadWarehouseSuccess(warehouses));
      })
      .catch((error) => {
        throw error;
      });
  };
}
