import React from "react";
import Button from "@mui/material/Button";
import CreateProductDialog from "./auxilaries/CreateProductForm";
import { connect } from "react-redux";
import { bindActionCreators } from "@reduxjs/toolkit";
import PropTypes from "prop-types";
import * as productActions from "../../redux/actions/productActions";
import BasicTable from "../common/TableMaker";

class ProductPage extends React.Component {
  state = {
    create: {
      shouldBeCreated: false,
    },
    product: {
      name: "",
      description: "",
      price: 0,
      productTypeId: 0,
    },
    productList: [],
  };

  handleOpen = () => {
    const create = { ...this.state.create, shouldBeCreated: true };
    this.setState({ create });
  };

  handleClose = () => {
    const create = { ...this.state.create, shouldBeCreated: false };
    this.setState({ create });
  };

  handleOnChange = (event) => {
    const name = event.target.id;
    const value = event.target.value;
    const product = { ...this.state.product, [name]: value };
    this.setState({ product });
  };

  handleSubmit = (event) => {
    event.preventDefault();
    this.props.actions.createProduct(this.state.product);
    // const list = [...this.state.productList];
    // list.push(this.state.product);
    // this.setState({ ...this.state, productList: list });
    this.handleClose();
  };

  render() {
    return (
      <React.Fragment>
        <h2>Product Page</h2>
        <Button onClick={this.handleOpen}>Add new product</Button>
        <CreateProductDialog
          open={this.state.create.shouldBeCreated}
          handleClose={this.handleClose}
          handleSubmit={this.handleSubmit}
          handleOnChange={this.handleOnChange}
        />
        {<BasicTable rows={this.props.products} />}
      </React.Fragment>
    );
  }
}

ProductPage.propTypes = {
  products: PropTypes.array.isRequired,
  actions: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
  return {
    products: state.products,
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(productActions, dispatch),
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(ProductPage);
