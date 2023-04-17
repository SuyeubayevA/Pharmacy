import * as types from "../actions/actionTypes";

export default function warehouseReducer(state = [], action) {
  switch (action.type) {
    case types.CREATE_WAREHOUSE:
      return [...state, { ...action.warehouse }];
    case types.LOAD_WAREHOUSES_SUCCESS:
      return action.warehouses;
    default:
      return state;
  }
}
