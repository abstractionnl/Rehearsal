import {NgModule} from "@angular/core";
import {FormValidationStyleDirective} from "./directives/form-validation-style.directive";

@NgModule({
    declarations: [
        FormValidationStyleDirective
    ],
    exports: [
        FormValidationStyleDirective
    ]
})
export class SharedModule {

}
