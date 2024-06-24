export class Model {
  products:Array<Product>;

  constructor() {
    this.products = [
      // new Product(1,"Samsung S5",2000,true),
      // new Product(2,"Samsung S6",3000,true),
      // new Product(3,"Samsung S7",4000,true),
      // new Product(4,"Samsung S8",5000,true),
      // new Product(5,"Samsung S9",6000,true),
      // new Product(6,"Samsung S10",7000,true),

    ]
  }
}
export class Product {
  productId:number;
  name:string;
  price:number;
  isActive:boolean;

  constructor(productId:number, name:string, price:number, isActive:boolean) {
    this.productId = productId;
    this.name = name;
    this.price = price;
    this.isActive = isActive;
  }
}

