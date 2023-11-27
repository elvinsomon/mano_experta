import 'package:flutter/material.dart';
import '../data/models/professional.dart';
import '../data/service/professional_service.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);
  @override
  State<HomePage> createState() => _HomePageState();
}
class _HomePageState extends State<HomePage> {
  List<Professional>? professionals;
  bool isLoaded = false;
  @override
  void initState() {
    super.initState();
    loadProfessionals();
  }
  Future<void> loadProfessionals() async {
    final professionalService = ProfessionalService();
    professionals = await professionalService.getProfessionalsByCaregory();
    setState(() {
      isLoaded = true;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: isLoaded && professionals != null
          ? SafeArea(
              child: Column(
                children: [
                  const SizedBox(height: 8),
                  const Row(
                    children: [
                      Expanded(child: Text('Name', textAlign: TextAlign.center)),
                      Expanded(child: Text('Apellido', textAlign: TextAlign.center)),
                      Expanded(child: Text('Telefono', textAlign: TextAlign.center)),
                      Expanded(child: Text('Email', textAlign: TextAlign.center)),
                    ],
                  ),
                  const SizedBox(height: 8),
                  Expanded(
                    child: SingleChildScrollView(
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Column(
                          children: professionals!
                              .map(
                                (professional) => Column(
                                  children: [
                                    const SizedBox(height: 4),
                                    Row(
                                      children: [
                                        Expanded(child: Text(professional.firstName ?? 'No name')),
                                        Expanded(
                                          child: Text(professional.lastName ?? 'No last name'),
                                        ),
                                        Expanded(
                                          child: Text(professional.phoneNumbers.firstOrNull!.number.toString() ?? 'No phone number'),
                                        ),
                                        Expanded(
                                            child: Text(professional.emails.firstOrNull!.address.toString() ?? 'No email')),
                                      ],
                                    ),
                                    const SizedBox(height: 4),
                                    const Divider(),
                                  ],
                                ),
                              )
                              .toList(),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            )
          : const Center(child: CircularProgressIndicator()),
    );
  }
}