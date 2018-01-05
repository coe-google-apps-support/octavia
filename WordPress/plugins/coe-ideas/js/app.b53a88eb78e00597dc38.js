webpackJsonp([1],{"2uFj":function(e,t){e.exports={IDEAS_API:"http://localhost:2117/api/Ideas"}},"4+hh":function(e,t){},BmlK:function(e,t){},NHnr:function(e,t,i){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a=i("7+uW"),n={render:function(){var e=this.$createElement,t=this._self._c||e;return t("div",{attrs:{id:"app-ideas"}},[t("router-view")],1)},staticRenderFns:[]},s=i("VU/8")({name:"app"},n,!1,function(e){i("BmlK")},null,null).exports,r=i("/ocq"),o=i("mtWM"),d=i.n(o),l=i("2uFj"),c=i.n(l),m=d.a.create({baseURL:c.a.IDEAS_API,headers:{AuthorizationEncrypted:"true"}});m.setUserInfo=function(e){d.a.defaults.headers.common.Authorization="Bearer "+(e||{}).auth};var u=m,f=i("ESwS"),v=i("+cKO"),p={name:"NewIdea",mixins:[f.validationMixin],data:function(){return{sending:!1,form:{title:null,description:null,tags:[]}}},validations:{form:{title:{required:v.required,minLength:Object(v.minLength)(3),maxLength:Object(v.maxLength)(255)},description:{required:v.required,minLength:Object(v.minLength)(3)}}},methods:{getValidationClass:function(e){var t=this.$v.form[e];if(t)return{"md-invalid":t.$invalid&&t.$dirty}},clearForm:function(){this.$v.$reset(),this.form.title=null,this.form.description=null,this.form.tags=[]},saveIdea:function(){var e=this;this.sending=!0,console.log("saving new idea"),u.post("",{title:this.form.title,description:this.form.description}).then(function(t){console.log("new idea saved!"),e.sending=!1}).catch(function(t,i){e.sending=!1,console.debug(t),console.debug(i)})},validateIdea:function(){this.$v.$touch(),this.$v.$invalid||this.saveIdea()}}},h={render:function(){var e=this,t=e.$createElement,i=e._self._c||t;return i("div",[i("form",{staticClass:"md-layout-row md-gutter",attrs:{novalidate:""}},[i("md-card-content",[i("div",{staticClass:"md-layout-row md-layout-wrap md-gutter"},[i("div",{staticClass:"md-flex md-flex-small-100"},[i("md-field",{class:e.getValidationClass("title")},[i("label",{attrs:{for:"idea-title"}},[e._v("Idea")]),e._v(" "),i("md-input",{attrs:{name:"title",id:"idea-title"},model:{value:e.form.title,callback:function(t){e.$set(e.form,"title",t)},expression:"form.title"}}),e._v(" "),e.$v.form.title.required?e.$v.form.title.minlength?e._e():i("span",{staticClass:"md-error"},[e._v("Invalid title")]):i("span",{staticClass:"md-error"},[e._v("Title is required")])],1)],1),e._v(" "),i("div",{staticClass:"md-flex md-flex-small-100"},[i("md-field",{class:e.getValidationClass("description")},[i("label",{attrs:{for:"idea-desc"}},[e._v("Description")]),e._v(" "),i("md-textarea",{attrs:{name:"description",id:"idea-desc"},model:{value:e.form.description,callback:function(t){e.$set(e.form,"description",t)},expression:"form.description"}}),e._v(" "),e.$v.form.description.required?e.$v.form.description.minlength?e._e():i("span",{staticClass:"md-error"},[e._v("Invalid Description")]):i("span",{staticClass:"md-error"},[e._v("Description is required")])],1)],1),e._v(" "),i("md-chips",{attrs:{name:"tags",id:"idea-tags","md-placeholder":"Add tag..."},model:{value:e.form.tags,callback:function(t){e.$set(e.form,"tags",t)},expression:"form.tags"}})],1)]),e._v(" "),e.sending?i("md-progress-bar",{attrs:{"md-mode":"indeterminate"}}):e._e(),e._v(" "),i("md-card-actions",[i("md-button",{staticClass:"md-primary",attrs:{type:"submit",disabled:e.sending},on:{click:function(t){t.preventDefault(),e.saveIdea(t)}}},[e._v("Submit idea")])],1)],1)])},staticRenderFns:[]},g=i("VU/8")(p,h,!1,function(e){i("qtYA")},"data-v-c597401a",null).exports;a.default.use(r.a);var _=new r.a({routes:[{path:"/",name:"NewIdea",component:g}]}),w=i("Lgyv"),I=i.n(w),b=(i("4+hh"),i("VLml"),this);a.default.config.productionTip=!1,a.default.use(I.a);var $=new a.default({el:"#coe-idea-new",router:_,template:"<AppNewIdea/>",components:{AppNewIdea:s},methods:{setUserInfo:function(e){var t=b;console.log(t),u.setUserInfo(e)}}}),x=window.coe;x||(x={},window.coe=x);var C=x.ideas;C||(C={},x.ideas=C),C.appNew=$},VLml:function(e,t){},qtYA:function(e,t){}},["NHnr"]);
//# sourceMappingURL=app.b53a88eb78e00597dc38.js.map