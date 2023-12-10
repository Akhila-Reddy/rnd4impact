import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Component({
selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit
{

    private readonly _authenticationService: AuthenticationService;

constructor(private readonly _authenticationService: AuthenticationService) { }

ngOnInit() {
}

login() {
    this._authenticationService.login('username', 'password').subscribe(
        () => console.log('Login successful'),
        error => console.log(error)
    );
}
}