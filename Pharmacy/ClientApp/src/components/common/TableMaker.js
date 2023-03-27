import * as React from "react";
import * as types from "../../entityTypes";
import ProductTable from "./tables/ProductTable";
import ProductTypeTable from "./tables/ProductTypeTable";
import WarehouseTable from "./tables/WarehouseTable";
import SalesInfoTable from "./tables/SalesInfoTable";

export default function BasicTable({ rows, type }) {
  if (type === types.PRODUCTS) {
    return <ProductTable rows={rows} />;
  }
  if (type === types.PRODUCTTYPES) {
    return <ProductTypeTable rows={rows} />;
  }
  if (type === types.WAREHOUSES) {
    return <WarehouseTable rows={rows} />;
  }
  if (type === types.PRODUCTINFO) {
    return <SalesInfoTable rows={rows} />;
  }
}
