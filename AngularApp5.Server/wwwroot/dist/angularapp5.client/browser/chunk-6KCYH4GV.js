import{a as T}from"./chunk-AUDLG5KQ.js";import{Ia as i,Ja as r,Ra as n,Sa as a,U as s,V as u,ba as f,db as k,ga as b,gb as v,ib as g,ka as y,oa as t,pa as e,ya as x}from"./chunk-4WBI4IRS.js";function I(l,m){if(l&1&&(t(0,"ul",4)(1,"li",5)(2,"strong",6),i(3,"Suma produkt\xF3w"),e(),t(4,"strong"),i(5),n(6,"async"),n(7,"currency"),e()(),t(8,"li",5)(9,"strong",6),i(10,"Koszt dostawy"),e(),t(11,"strong"),i(12),n(13,"async"),n(14,"currency"),e()(),t(15,"li",5)(16,"strong",6),i(17,"Finalny koszt zam\xF3wienia"),e(),t(18,"strong"),i(19),n(20,"async"),n(21,"currency"),e()()()),l&2){let o,p,d,c=x();s(5),r(a(7,5,(o=a(6,3,c.basketTotal$))==null?null:o.subtotal)),s(7),r(a(14,9,(p=a(13,7,c.basketTotal$))==null?null:p.shipping)),s(7),r(a(21,13,(d=a(20,11,c.basketTotal$))==null?null:d.total))}}var S=class l{constructor(m){this.basketService=m}basketTotal$;ngOnInit(){this.basketTotal$=this.basketService.basketTotal$}static \u0275fac=function(o){return new(o||l)(u(T))};static \u0275cmp=f({type:l,selectors:[["app-order-totals"]],standalone:!1,decls:7,vars:3,consts:[[1,"bg-light","px-4","py-3","text-uppercase","font-weight-bold"],[1,"p-4"],[1,"font-italic","mb-4"],["class","list-unstyled mb-4",4,"ngIf"],[1,"list-unstyled","mb-4"],[1,"d-flex","justify-content-between","py-3","border-bottom"],[1,"text-muted"]],template:function(o,p){o&1&&(t(0,"div",0),i(1,` Podsumowanie zam\xF3wienia
`),e(),t(2,"div",1)(3,"p",2),i(4," Koszty wysy\u0142kki zostan\u0105 naliczone po jej wyborze "),e(),b(5,I,22,15,"ul",3),n(6,"async"),e()),o&2&&(s(5),y("ngIf",a(6,1,p.basketTotal$)))},dependencies:[k,v,g],encapsulation:2})};export{S as a};
