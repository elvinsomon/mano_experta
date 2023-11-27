import '../models/professional.dart';
import '../provider/professional_api.dart';

class ProfessionalService {
  final _api = ProfessionalApi();
  Future<List<Professional>?> getProfessionalsByCaregory() async {
    return _api.getGetProfessionalByCategory();
  }
}