import {Directive, OnDestroy, TemplateRef, ViewContainerRef} from '@angular/core';
import {AuthModuleState, selectIsLoggedIn} from "../store/auth.state";
import {Store} from "@ngrx/store";
import {Subscription} from "rxjs/index";

@Directive({
    selector: '[ifLoggedIn]'
})
export class IfLoggedInDirective implements OnDestroy {
    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private store: Store<AuthModuleState>
    ) {
        this.subscription = this.store
            .select(selectIsLoggedIn)
            .subscribe(val => this.updateView(val));
    }

    private subscription: Subscription;
    private hasView: boolean;

    updateView(show: boolean) {
         if (show && !this.hasView) {
             this.viewContainer.createEmbeddedView(this.templateRef);
             this.hasView = true;
         } else if (!show && this.hasView) {
             this.viewContainer.clear();
             this.hasView = false;
         }
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
}
