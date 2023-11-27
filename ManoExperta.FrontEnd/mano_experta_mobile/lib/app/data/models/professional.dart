// To parse this JSON data, do
//
//     final professional = professionalFromJson(jsonString);

import 'dart:convert';

List<Professional> professionalFromJson(String str) => List<Professional>.from(json.decode(str).map((x) => Professional.fromJson(x)));

String professionalToJson(List<Professional> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class Professional {
    String id;
    String userName;
    String firstName;
    String lastName;
    int type;
    List<Category> category;
    List<PhoneNumber> phoneNumbers;
    List<Email> emails;
    List<WorkingHour> workingHours;

    Professional({
        required this.id,
        required this.userName,
        required this.firstName,
        required this.lastName,
        required this.type,
        required this.category,
        required this.phoneNumbers,
        required this.emails,
        required this.workingHours,
    });

    factory Professional.fromJson(Map<String, dynamic> json) => Professional(
        id: json["id"],
        userName: json["userName"],
        firstName: json["firstName"],
        lastName: json["lastName"],
        type: json["type"],
        category: List<Category>.from(json["category"].map((x) => Category.fromJson(x))),
        phoneNumbers: List<PhoneNumber>.from(json["phoneNumbers"].map((x) => PhoneNumber.fromJson(x))),
        emails: List<Email>.from(json["emails"].map((x) => Email.fromJson(x))),
        workingHours: List<WorkingHour>.from(json["workingHours"].map((x) => WorkingHour.fromJson(x))),
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "userName": userName,
        "firstName": firstName,
        "lastName": lastName,
        "type": type,
        "category": List<dynamic>.from(category.map((x) => x.toJson())),
        "phoneNumbers": List<dynamic>.from(phoneNumbers.map((x) => x.toJson())),
        "emails": List<dynamic>.from(emails.map((x) => x.toJson())),
        "workingHours": List<dynamic>.from(workingHours.map((x) => x.toJson())),
    };
}

class Category {
    String code;
    String name;
    dynamic description;

    Category({
        required this.code,
        required this.name,
        required this.description,
    });

    factory Category.fromJson(Map<String, dynamic> json) => Category(
        code: json["code"],
        name: json["name"],
        description: json["description"],
    );

    Map<String, dynamic> toJson() => {
        "code": code,
        "name": name,
        "description": description,
    };
}

class Email {
    String address;

    Email({
        required this.address,
    });

    factory Email.fromJson(Map<String, dynamic> json) => Email(
        address: json["address"],
    );

    Map<String, dynamic> toJson() => {
        "address": address,
    };
}

class PhoneNumber {
    String number;

    PhoneNumber({
        required this.number,
    });

    factory PhoneNumber.fromJson(Map<String, dynamic> json) => PhoneNumber(
        number: json["number"],
    );

    Map<String, dynamic> toJson() => {
        "number": number,
    };
}

class WorkingHour {
    int day;
    int start;
    int end;

    WorkingHour({
        required this.day,
        required this.start,
        required this.end,
    });

    factory WorkingHour.fromJson(Map<String, dynamic> json) => WorkingHour(
        day: json["day"],
        start: json["start"],
        end: json["end"],
    );

    Map<String, dynamic> toJson() => {
        "day": day,
        "start": start,
        "end": end,
    };
}
