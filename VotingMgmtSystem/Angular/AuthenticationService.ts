import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
@Injectable({
providedIn: 'root'
})
export class AuthenticationService
{
    private readonly _http: HttpClient;

constructor(private readonly _http: HttpClient) {}

login(username: string, password: string)
    {
        return this._http.post(`api / users / login`, {
        username: username,
        password: password
        });
    }

    register(user: User)
    {
        return this._http.post(`api / users / register`, user);
    }

    isAuthenticated()
    {
        return this._http.get(`api / users / authenticated`).toPromise().then(response => response.data);
    }

    logout()
    {
        return this._http.post(`api / users / logout`);
    }
}