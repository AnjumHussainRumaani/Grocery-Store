export interface ProductDetails {
    id: string;
    name: string;
    price: number;
    quantity: number;
    expiryDate: Date;
    purchaseDate: Date;
    aisleId: string;  // Foreign key linking to Aisle
  }

export interface AisleDetails {
    id: string;
    name: string;
    productIds: string[];
  }