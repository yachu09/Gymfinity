export interface IBasket {
  id: string
  items: IBasketItem[]
}
import { v4 as uuidv4} from 'uuid'
export interface IBasketItem {
  id: number
  productName: string
  price: number
  quantity: number
  imageUrl: string
  brand: string
  type: string
}

export class Basket implements IBasket {
    id = uuidv4();
    items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
