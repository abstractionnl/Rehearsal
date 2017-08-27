import {
    ContentChildren, Directive, ElementRef, OnInit, QueryList, Renderer2, AfterViewInit, OnDestroy
} from "@angular/core";
import { NgModel } from "@angular/forms";

import { Subject } from "rxjs/Subject";
import { Subscription } from "rxjs/Subscription";
import "rxjs/add/operator/distinctUntilChanged";

@Directive({
    selector: '[formValidationStyle]'
})
export class FormValidationStyleDirective implements OnInit, AfterViewInit, OnDestroy {
    @ContentChildren(NgModel, {descendants: true}) modelComponents: QueryList<NgModel>;

    static VALID_STYLE: string = 'has-success';
    static ERROR_STYLE: string = 'has-error';

    private errorSubject: Subject<boolean> = new Subject<boolean>();
    private errorSubjectSubscription: Subscription;

    constructor(private element: ElementRef, private renderer: Renderer2) { }

    ngOnInit(): void {
        this.errorSubjectSubscription = this.errorSubject
            .distinctUntilChanged()
            .subscribe((hasError) => {
                if (hasError) {
                    this.renderer.addClass(this.element.nativeElement, FormValidationStyleDirective.ERROR_STYLE);
                } else {
                    this.renderer.removeClass(this.element.nativeElement, FormValidationStyleDirective.ERROR_STYLE);
                }
            });
    }

    ngOnDestroy(): void {
        this.errorSubjectSubscription.unsubscribe();
    }

    ngAfterViewInit() {
        this.modelComponents.changes.subscribe(_ => this.update());
        //this.modelComponents.forEach(ngModel => ngModel..subscribe(() => this.update()));
        this.modelComponents.forEach(ngModel => ngModel.valueChanges.subscribe(() => this.update()));
        this.update();
    }

    private update() {
        if (!this.modelComponents) return;

        let hasError = this.modelComponents.some(x => !x.valid && (x.touched || x.dirty));
        let isValid = !this.modelComponents.some(x => !x.valid);

        this.errorSubject.next(hasError);
    }

}
