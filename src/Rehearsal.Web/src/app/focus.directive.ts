import {Directive, ElementRef, EventEmitter, Input, Renderer, Inject, OnInit} from '@angular/core';

@Directive({
    selector: '[focus]'
})
export class FocusDirective implements OnInit {
    constructor(@Inject(ElementRef) private element: ElementRef, private renderer: Renderer) { }

    @Input('focus') focusEvent: EventEmitter<void>;

    ngOnInit() {
        this.focusEvent.subscribe(event => {
            // Deliberate delay here, in case the element is still disabled
            setTimeout(() =>
                this.renderer.invokeElementMethod(this.element.nativeElement, 'focus', []),
                10
            )
        });
    }

}
