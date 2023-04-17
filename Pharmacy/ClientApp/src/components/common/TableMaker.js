import * as React from "react";
import * as types from "../../entityTypes";
import ProductTable from "./tables/ProductTable";
import ProductTypeTable from "./tables/ProductTypeTable";
import WarehouseTable from "./tables/WarehouseTable";
import SalesInfoTable from "./tables/SalesInfoTable";
import DiscountTable from "./tables/DIscountTable";

export default function BasicTable({ rows, type, deleteItem }) {
  if (type === types.PRODUCTS) {
    return <ProductTable rows={rows} deleteItem={deleteItem} />;
  }
  if (type === types.PRODUCTTYPES) {
    return <ProductTypeTable rows={rows} deleteItem={deleteItem} />;
  }
  if (type === types.WAREHOUSES) {
    return <WarehouseTable rows={rows} />;
  }
  if (type === types.PRODUCTINFO) {
    return <SalesInfoTable rows={rows} />;
  }
  if (type === types.PRODUCTDISCOUNT) {
    return <DiscountTable rows={rows} />;
  }
}
