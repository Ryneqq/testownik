use rocket::{Route, routes};
use rocket::Data;
use rocket::request::Form;
use rocket::Response;
// use rocket_contrib::json::{JsonError, Json};
use rocket::http::uri::Origin as URI;
use rocket::http::Method;
use std::io::Cursor;
use rocket::State;
use std::borrow::Borrow;

pub fn set_up() -> Vec<Route> {
    routes![
        get_test,
        post_test
    ]
}

#[get("/test")]
fn get_test<'a, 'r>(uri: &'a URI) -> Response<'r> {
    respond_with("It is working af")
}

#[post("/test", format="text/plain", data="<nice>")]
fn post_test<'a, 'r>(uri: &'a URI, mut nice: Data) -> Response<'r> {
    let data = nice.to_string();
    let body = format!("It is working more than af, you have send me this shiet: '{}'", data);

    respond_with(body)
}

fn respond_with<'r>(message: impl AsRef<str>) -> Response<'r> {
    let mut reponse = Response::new();
    reponse.set_sized_body(Cursor::new(message.as_ref().to_string()));

    reponse
}
