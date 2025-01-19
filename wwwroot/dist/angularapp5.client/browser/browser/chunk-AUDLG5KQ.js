import{Ab as B,B as h,f as u,l as k,nb as b,y as I}from"./chunk-4WBI4IRS.js";var i=[];for(let r=0;r<256;++r)i.push((r+256).toString(16).slice(1));function g(r,t=0){return(i[r[t+0]]+i[r[t+1]]+i[r[t+2]]+i[r[t+3]]+"-"+i[r[t+4]]+i[r[t+5]]+"-"+i[r[t+6]]+i[r[t+7]]+"-"+i[r[t+8]]+i[r[t+9]]+"-"+i[r[t+10]]+i[r[t+11]]+i[r[t+12]]+i[r[t+13]]+i[r[t+14]]+i[r[t+15]]).toLowerCase()}var l,x=new Uint8Array(16);function m(){if(!l){if(typeof crypto>"u"||!crypto.getRandomValues)throw new Error("crypto.getRandomValues() not supported. See https://github.com/uuidjs/uuid#getrandomvalues-not-supported");l=crypto.getRandomValues.bind(crypto)}return l(x)}var v=typeof crypto<"u"&&crypto.randomUUID&&crypto.randomUUID.bind(crypto),d={randomUUID:v};function y(r,t,e){if(d.randomUUID&&!t&&!r)return d.randomUUID();r=r||{};let a=r.random??r.rng?.()??m();if(a.length<16)throw new Error("Random bytes length must be >= 16");if(a[6]=a[6]&15|64,a[8]=a[8]&63|128,t){if(e=e||0,e<0||e+16>t.length)throw new RangeError(`UUID byte range ${e}:${e+15} is out of buffer bounds`);for(let s=0;s<16;++s)t[e+s]=a[s];return t}return g(a)}var p=y;var o=class{id=p();items=[]};var f=class r{constructor(t){this.http=t}baseUrl=B.apiUrl;basketSource=new u(null);basket$=this.basketSource.asObservable();basketTotalSource=new u(null);basketTotal$=this.basketTotalSource.asObservable();shipping=0;setShippingPrice(t){this.shipping=t.price,this.calculateTotals()}getBasket(t){return this.http.get(this.baseUrl+"basket?id="+t).pipe(k(e=>{this.basketSource.next(e),this.calculateTotals()}))}setBasket(t){return this.http.post(this.baseUrl+"basket/update",t).subscribe(e=>{this.basketSource.next(e),this.calculateTotals()},e=>{console.log(e)})}getCurrentBasketValue(){return this.basketSource.value}addItemToBasket(t,e=1){let a=this.mapProductItemToBasketItem(t,e),s=this.getCurrentBasketValue()??this.createBasket();s.items=this.addOrUpdateItem(s.items,a,e),this.setBasket(s)}incrementItemQuantity(t){let e=this.getCurrentBasketValue(),a=e.items.findIndex(s=>s.id===t.id);e.items[a].quantity++,this.setBasket(e)}decrementItemQuantity(t){let e=this.getCurrentBasketValue(),a=e.items.findIndex(s=>s.id===t.id);e.items[a].quantity>1?(e.items[a].quantity--,this.setBasket(e)):this.removeItemFromBasket(t)}removeItemFromBasket(t){let e=this.getCurrentBasketValue();e?.items.some(a=>a.id===t.id)&&(e.items=e.items.filter(a=>a.id!==t.id),e.items.length>0?this.setBasket(e):this.deleteBasket(e))}deleteBasket(t){return this.http.delete(this.baseUrl+"basket?id="+t.id).subscribe(e=>{console.log(e),this.deleteLocalBasket()},e=>{console.log(e)})}deleteLocalBasket(){this.basketSource.next(null),this.basketTotalSource.next(null),localStorage.removeItem("basket_id")}calculateTotals(){let t=this.getCurrentBasketValue(),e=this.shipping,a=t.items.reduce((n,c)=>c.price*c.quantity+n,0),s=a+e;this.basketTotalSource.next({shipping:e,total:s,subtotal:a})}addOrUpdateItem(t,e,a){let s=t.findIndex(n=>n.id===e.id);return s===-1?(e.quantity=a,t.push(e)):t[s].quantity+=a,t}createBasket(){let t=new o;return localStorage.setItem("basket_id",t.id),t}mapProductItemToBasketItem(t,e){return{id:t.id,productName:t.name,price:t.price,imageUrl:t.imageUrl,quantity:e,brand:t.productBrand,type:t.productType}}static \u0275fac=function(e){return new(e||r)(h(b))};static \u0275prov=I({token:r,factory:r.\u0275fac,providedIn:"root"})};export{f as a};
