import { Component } from '@angular/core';
import { Auth } from "./auth/auth.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    title = 'Tour of Heroes';

    constructor(public auth: Auth) {

    }
}
